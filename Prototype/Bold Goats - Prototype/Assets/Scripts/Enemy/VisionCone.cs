using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

[RequireComponent(typeof(EnemyVision))]
public class VisionCone : MonoBehaviour
{

    public float meshResolution;

    public LayerMask mask;

    EnemyVision enemyVision;

    public MeshFilter viewMeshFilter;
    private Mesh viewMesh;

    public int edgeFindIterations;
    public float edgeTolerance;

    private void Start()
    {
        enemyVision = GetComponent<EnemyVision>();

        viewMesh = new Mesh();
        viewMesh.name = "Vision Cone";
        viewMeshFilter.mesh = viewMesh;

    }

    private void LateUpdate()
    {
        DrawVisionCone();
    }

    public void DrawVisionCone()
    {
        int stepCount = Mathf.RoundToInt(enemyVision.maxSightAngle * meshResolution);
        float stepDegrees = enemyVision.maxSightAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();

        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - enemyVision.maxSightAngle / 2 + stepDegrees * i;

            ViewCastInfo viewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeToleranceExceeded = Mathf.Abs(oldViewCast.distance - viewCast.distance) > edgeTolerance;
                if (oldViewCast.hit != viewCast.hit || (oldViewCast.hit && viewCast.hit && edgeToleranceExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, viewCast);

                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(viewCast.point);

            oldViewCast = viewCast;

        }


        int vertexNumber = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexNumber];
        int[] tris = new int[(vertexNumber - 2) * 3];

        vertices[0] = Vector3.zero;

        
        for (int i = 0; i < vertexNumber - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexNumber - 2)
            {
                tris[i * 3] = 0;
                tris[i * 3 + 1] = i + 1;
                tris[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = tris;
        viewMesh.RecalculateNormals();


    }
        public Vector3 DirFromAngle(float angle, bool isGlobal)
        {
            if (!isGlobal)
            {
                angle += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
        }

    EdgeInfo FindEdge(ViewCastInfo min, ViewCastInfo max)
    {
        float minAngle = min.angle;
        float maxAngle = max.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        float angle = 0f;

        for (int i = 0; i < edgeFindIterations; i++)
        {
            angle = (minAngle + maxAngle) / 2;
            ViewCastInfo viewCast = ViewCast(angle);


            bool edgeToleranceExceeded = Mathf.Abs(min.distance - viewCast.distance) > edgeTolerance;
            if (viewCast.hit == min.hit && !edgeToleranceExceeded)
            {
                minAngle = angle;
                minPoint = viewCast.point;
            } else
            {
                maxAngle = angle;
                maxPoint = viewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);

    }

    ViewCastInfo ViewCast(float angle)
    {
        Vector3 direction = DirFromAngle(angle, true);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, enemyVision.visionLength, mask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, angle);
        } else
        {
            return new ViewCastInfo(false, transform.position + direction * enemyVision.visionLength, enemyVision.visionLength, angle);
        }
    }
    

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }


}
