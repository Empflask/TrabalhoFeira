using System.Collections.Generic;
using UnityEngine;

public class moverobjeto : MonoBehaviour
{
    [Header("Movimento")]
    public int passoMovimento = 1;
    [Tooltip("X = minimo, Y = maximo, em coordenadas do mundo.")]
    public Vector2 limiteX = new Vector2(-10f, 10f);
    [Tooltip("X = minimo, Y = maximo, em coordenadas do mundo.")]
    public Vector2 limiteZ = new Vector2(-10f, 10f);

    [Header("Cores")]
    public Color corHover = Color.yellow;
    public Color corArrastando = Color.green;
    public Color corSobreposicao = Color.red;
    public Color corContorno = Color.white;

    [Header("Visualizacao da colisao")]
    [Min(0.001f)]
    public float larguraContorno = 0.04f;

    private GameObject selectObject;
    private GameObject draggingObject;
    private GameObject hoverObject;
    private GameObject releasedObject;
    private GameObject colliderOutlineObject;
    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;
    private Vector3 dragStartPoint;
    private Plane dragPlane;
    private bool cameraWarningShown;
    private Material outlineMaterial;

    private static readonly int[,] BoxEdges =
    {
        { 0, 1 }, { 0, 2 }, { 1, 3 }, { 2, 3 },
        { 4, 5 }, { 4, 6 }, { 5, 7 }, { 6, 7 },
        { 0, 4 }, { 1, 5 }, { 2, 6 }, { 3, 7 }
    };

    private readonly Dictionary<GameObject, List<MaterialColorState>> materialStates =
        new Dictionary<GameObject, List<MaterialColorState>>();

    private class MaterialColorState
    {
        public readonly Material[] materials;
        public readonly Color[] originalColors;
        public readonly string[] colorProperties;

        public MaterialColorState(Renderer renderer)
        {
            materials = renderer.materials;
            originalColors = new Color[materials.Length];
            colorProperties = new string[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].HasProperty("_BaseColor"))
                {
                    colorProperties[i] = "_BaseColor";
                }
                else if (materials[i].HasProperty("_Color"))
                {
                    colorProperties[i] = "_Color";
                }

                if (colorProperties[i] != null)
                {
                    originalColors[i] = materials[i].GetColor(colorProperties[i]);
                }
            }
        }

        public void SetColor(Color color)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                if (colorProperties[i] != null)
                {
                    materials[i].SetColor(colorProperties[i], color);
                }
            }
        }

        public void RestoreOriginalColor()
        {
            for (int i = 0; i < materials.Length; i++)
            {
                if (colorProperties[i] != null)
                {
                    materials[i].SetColor(colorProperties[i], originalColors[i]);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && selectObject != null)
        {
            DeleteSelectedObject();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (draggingObject != null)
            {
                StopDragging();
            }
            else
            {
                StartDragging();
            }
        }

        if (draggingObject != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RotateDraggedObject();
            }

            MoveDraggedObject();
        }

        if (draggingObject == null)
        {
            UpdateHover();
        }
    }

    private void DeleteSelectedObject()
    {
        GameObject objectToDelete = selectObject;
        RestoreObjectColor(objectToDelete);
        materialStates.Remove(objectToDelete);

        if (draggingObject == objectToDelete)
        {
            draggingObject = null;
        }

        if (hoverObject == objectToDelete)
        {
            hoverObject = null;
        }

        if (releasedObject == objectToDelete)
        {
            releasedObject = null;
        }

        selectObject = null;
        ClearColliderOutline();
        Destroy(objectToDelete);
    }

    private void StartDragging()
    {
        if (!TryGetMouseRay(out Ray ray) || !Physics.Raycast(ray, out RaycastHit hit) ||
            !TryGetTaggedObject(hit.collider, out GameObject obj))
        {
            selectObject = null;
            ClearHover();
            return;
        }

        selectObject = obj;
        draggingObject = obj;
        hoverObject = null;
        if (IsQuadro(obj))
        {
            Vector3 position = obj.transform.position;
            position.z = GetZPermitidoDoQuadro(position.z);
            obj.transform.position = position;
            Physics.SyncTransforms();
        }

        dragStartPosition = obj.transform.position;
        dragStartRotation = obj.transform.rotation;
        dragPlane = new Plane(Vector3.up, dragStartPosition);

        if (dragPlane.Raycast(ray, out float distance))
        {
            dragStartPoint = ray.GetPoint(distance);
            CreateColliderOutline();
            UpdateDraggedObjectVisuals();
            Debug.Log("Objeto selecionado: " + selectObject.name);
        }
        else
        {
            draggingObject = null;
        }
    }

    private void RotateDraggedObject()
    {
        draggingObject.transform.Rotate(Vector3.up, 45f, Space.World);
        UpdateDraggedObjectVisuals();
    }

    private void MoveDraggedObject()
    {
        if (!TryGetMouseRay(out Ray ray) || !dragPlane.Raycast(ray, out float distance))
        {
            return;
        }

        Vector3 currentPoint = ray.GetPoint(distance);
        int step = Mathf.Max(1, passoMovimento);
        Vector3 position = dragStartPosition;
        position.x += Mathf.Round((currentPoint.x - dragStartPoint.x) / step) * step;
        float deltaZ = currentPoint.z - dragStartPoint.z;
        if (IsQuadro(draggingObject))
        {
            position.z = GetZPermitidoDoQuadro(dragStartPosition.z + deltaZ);
        }
        else
        {
            position.z += Mathf.Round(deltaZ / step) * step;
            position.z = Mathf.Clamp(position.z, Mathf.Min(limiteZ.x, limiteZ.y), Mathf.Max(limiteZ.x, limiteZ.y));
        }

        position.x = Mathf.Clamp(position.x, Mathf.Min(limiteX.x, limiteX.y), Mathf.Max(limiteX.x, limiteX.y));
        draggingObject.transform.position = position;
        UpdateDraggedObjectVisuals();
    }

    private bool IsQuadro(GameObject obj)
    {
        return obj.name == "quadro" || obj.name == "quadro(Clone)";
    }

    private float GetZPermitidoDoQuadro(float z)
    {
        const float zFrente = 6f;
        const float zFundo = -7.3f;
        return Mathf.Abs(z - zFrente) <= Mathf.Abs(z - zFundo) ? zFrente : zFundo;
    }

    private void StopDragging()
    {
        if (draggingObject == null)
        {
            return;
        }

        if (IsOverlappingAnotherObject())
        {
            draggingObject.transform.position = dragStartPosition;
            draggingObject.transform.rotation = dragStartRotation;
            Physics.SyncTransforms();
        }

        RestoreObjectColor(draggingObject);
        ClearColliderOutline();
        releasedObject = draggingObject;
        draggingObject = null;
    }

    private void UpdateDraggedObjectVisuals()
    {
        bool overlapping = IsOverlappingAnotherObject();
        SetObjectColor(draggingObject, overlapping ? corSobreposicao : corArrastando);
        SetColliderOutlineColor(overlapping ? corSobreposicao : corContorno);
    }

    private bool IsOverlappingAnotherObject()
    {
        Physics.SyncTransforms();
        Collider[] objectColliders = draggingObject.GetComponentsInChildren<Collider>();

        foreach (Collider ownCollider in objectColliders)
        {
            if (!ownCollider.enabled || !ownCollider.gameObject.activeInHierarchy)
            {
                continue;
            }

            Collider[] nearbyColliders = Physics.OverlapBox(
                ownCollider.bounds.center,
                ownCollider.bounds.extents,
                Quaternion.identity,
                Physics.AllLayers,
                QueryTriggerInteraction.Collide);

            foreach (Collider otherCollider in nearbyColliders)
            {
                if (otherCollider == ownCollider ||
                    !otherCollider.enabled ||
                    !otherCollider.gameObject.activeInHierarchy ||
                    IsPartOfDraggedObject(otherCollider) ||
                    Physics.GetIgnoreLayerCollision(ownCollider.gameObject.layer, otherCollider.gameObject.layer) ||
                    Physics.GetIgnoreCollision(ownCollider, otherCollider))
                {
                    continue;
                }

                if (Physics.ComputePenetration(
                    ownCollider,
                    ownCollider.transform.position,
                    ownCollider.transform.rotation,
                    otherCollider,
                    otherCollider.transform.position,
                    otherCollider.transform.rotation,
                    out _,
                    out _))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsPartOfDraggedObject(Collider collider)
    {
        return collider.transform == draggingObject.transform ||
               collider.transform.IsChildOf(draggingObject.transform);
    }

    private void CreateColliderOutline()
    {
        Collider[] colliders = draggingObject.GetComponentsInChildren<Collider>();
        if (colliders.Length == 0)
        {
            Debug.LogWarning("O objeto selecionado não possui Collider para mostrar.");
            return;
        }

        Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
        if (shader == null)
        {
            shader = Shader.Find("Sprites/Default");
        }

        if (shader == null)
        {
            Debug.LogError("Não foi encontrado um shader para desenhar o contorno do Collider.");
            return;
        }

        if (outlineMaterial == null)
        {
            outlineMaterial = new Material(shader);
        }

        colliderOutlineObject = new GameObject("Contorno dos Colliders");
        LineRenderer[] lines = new LineRenderer[colliders.Length * BoxEdges.GetLength(0)];
        int lineIndex = 0;

        foreach (Collider collider in colliders)
        {
            for (int edge = 0; edge < BoxEdges.GetLength(0); edge++)
            {
                GameObject lineObject = new GameObject("Aresta do Collider");
                lineObject.transform.SetParent(colliderOutlineObject.transform, false);

                LineRenderer line = lineObject.AddComponent<LineRenderer>();
                line.useWorldSpace = true;
                line.positionCount = 2;
                line.sharedMaterial = outlineMaterial;
                line.startWidth = larguraContorno;
                line.endWidth = larguraContorno;
                lines[lineIndex++] = line;
            }
        }

        colliderOutlineObject.AddComponent<ColliderOutline>().Initialize(colliders, lines);
        SetColliderOutlineColor(corContorno);
    }

    private void SetColliderOutlineColor(Color color)
    {
        if (colliderOutlineObject == null)
        {
            return;
        }

        LineRenderer[] lines = colliderOutlineObject.GetComponentsInChildren<LineRenderer>();
        foreach (LineRenderer line in lines)
        {
            line.startColor = color;
            line.endColor = color;
            line.startWidth = larguraContorno;
            line.endWidth = larguraContorno;
        }
    }

    private void ClearColliderOutline()
    {
        if (colliderOutlineObject != null)
        {
            Destroy(colliderOutlineObject);
            colliderOutlineObject = null;
        }
    }

    private void OnDestroy()
    {
        if (outlineMaterial != null)
        {
            Destroy(outlineMaterial);
        }
    }

    private void UpdateHover()
    {
        GameObject target = null;
        if (TryGetMouseRay(out Ray ray) && Physics.Raycast(ray, out RaycastHit hit))
        {
            TryGetTaggedObject(hit.collider, out target);
        }

        if (target != releasedObject)
        {
            releasedObject = null;
        }
        else
        {
            target = null;
        }

        if (target == hoverObject)
        {
            return;
        }

        ClearHover();
        if (target != null)
        {
            hoverObject = target;
            SetObjectColor(hoverObject, corHover);
        }
    }

    private void ClearHover()
    {
        if (hoverObject != null)
        {
            RestoreObjectColor(hoverObject);
            hoverObject = null;
        }
    }

    private bool TryGetTaggedObject(Collider collider, out GameObject obj)
    {
        Transform candidate = collider.transform;
        while (candidate != null)
        {
            if (candidate.CompareTag("Objeto"))
            {
                obj = candidate.gameObject;
                return true;
            }

            candidate = candidate.parent;
        }

        obj = null;
        return false;
    }

    private bool TryGetMouseRay(out Ray ray)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            ray = new Ray();
            if (!cameraWarningShown)
            {
                Debug.LogError("Não foi encontrada uma câmera com a tag MainCamera.");
                cameraWarningShown = true;
            }

            return false;
        }

        cameraWarningShown = false;
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return true;
    }

    private void SetObjectColor(GameObject obj, Color color)
    {
        foreach (MaterialColorState state in GetMaterialStates(obj))
        {
            state.SetColor(color);
        }
    }

    private void RestoreObjectColor(GameObject obj)
    {
        foreach (MaterialColorState state in GetMaterialStates(obj))
        {
            state.RestoreOriginalColor();
        }
    }

    private List<MaterialColorState> GetMaterialStates(GameObject obj)
    {
        if (!materialStates.TryGetValue(obj, out List<MaterialColorState> states))
        {
            states = new List<MaterialColorState>();
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer renderer in renderers)
            {
                states.Add(new MaterialColorState(renderer));
            }

            materialStates.Add(obj, states);
        }

        return states;
    }

    private class ColliderOutline : MonoBehaviour
    {
        private Collider[] colliders;
        private LineRenderer[] lines;

        public void Initialize(Collider[] sourceColliders, LineRenderer[] outlineLines)
        {
            colliders = sourceColliders;
            lines = outlineLines;
            Update();
        }

        private void Update()
        {
            if (colliders == null || lines == null)
            {
                return;
            }

            int lineIndex = 0;
            foreach (Collider collider in colliders)
            {
                Vector3[] corners;
                if (collider is BoxCollider boxCollider)
                {
                    corners = GetBoxCorners(boxCollider);
                }
                else
                {
                    Bounds bounds = collider.bounds;
                    Vector3 min = bounds.min;
                    Vector3 max = bounds.max;
                    corners = new[]
                    {
                        new Vector3(min.x, min.y, min.z),
                        new Vector3(max.x, min.y, min.z),
                        new Vector3(min.x, max.y, min.z),
                        new Vector3(max.x, max.y, min.z),
                        new Vector3(min.x, min.y, max.z),
                        new Vector3(max.x, min.y, max.z),
                        new Vector3(min.x, max.y, max.z),
                        new Vector3(max.x, max.y, max.z)
                    };
                }

                for (int edge = 0; edge < BoxEdges.GetLength(0); edge++)
                {
                    LineRenderer line = lines[lineIndex++];
                    line.SetPosition(0, corners[BoxEdges[edge, 0]]);
                    line.SetPosition(1, corners[BoxEdges[edge, 1]]);
                }
            }
        }

        private Vector3[] GetBoxCorners(BoxCollider boxCollider)
        {
            Vector3 center = boxCollider.center;
            Vector3 halfSize = boxCollider.size * 0.5f;
            Vector3[] localCorners =
            {
                center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z),
                center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z),
                center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z),
                center + new Vector3(halfSize.x, halfSize.y, -halfSize.z),
                center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z),
                center + new Vector3(halfSize.x, -halfSize.y, halfSize.z),
                center + new Vector3(-halfSize.x, halfSize.y, halfSize.z),
                center + new Vector3(halfSize.x, halfSize.y, halfSize.z)
            };

            for (int i = 0; i < localCorners.Length; i++)
            {
                localCorners[i] = boxCollider.transform.TransformPoint(localCorners[i]);
            }

            return localCorners;
        }
    }
}
