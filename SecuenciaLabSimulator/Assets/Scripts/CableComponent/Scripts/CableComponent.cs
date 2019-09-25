using UnityEngine;
using System;
using System.Collections;

public class CableComponent : MonoBehaviour
{
    #region Class members

    [SerializeField] public bool showRender = true;
    [SerializeField] public GameObject startPoint;
    [SerializeField] public GameObject endPoint;
    [SerializeField] public Material cableMaterial;
    [SerializeField] public Color startColor = AuxiliarModulos.startColor;
    [SerializeField] public Color endColor = AuxiliarModulos.endColor;
    [SerializeField] public bool todoCableMismoColor = false;

    // Cable config
    [SerializeField] private float cableLength = 0.1f;
    [SerializeField] private int totalSegments = 5;
    [SerializeField] private float segmentsPerUnit = 2f;
    private int segments = 0;
    [SerializeField] private float cableWidth = 0.1f;
    [SerializeField] private float offsetLength = -8.0f;
    public float offsetX = 0.0f;
    public float offsetY = 0.0f;
    public float offsetZ = 0.13f;
    public bool diagramaSecuencial = false;

    // Solver config
    [SerializeField] private int verletIterations = 1;
    [SerializeField] private int solverIterations = 1;

    //[Range(0,3)]
    [SerializeField] private float stiffness = 1f;

    public LineRenderer line;
    private CableParticle[] points;

    #endregion

    #region Properties

    public GameObject StartPoint
    {
        get => startPoint;
        set => startPoint = value;
    }

    public GameObject EndPoint
    {
        get => endPoint;
        set => endPoint = value;
    }
    public int TotalSegments { get => totalSegments; set => totalSegments = value; }
    public float SegmentsPerUnit { get => segmentsPerUnit; set => segmentsPerUnit = value; }
    public float CableWidth { get => cableWidth; set => cableWidth = value; }
    public float OffsetLength { get => offsetLength; set => offsetLength = value; }
    public int VerletIterations { get => verletIterations; set => verletIterations = value; }
    public int SolverIterations { get => solverIterations; set => solverIterations = value; }
    public float Stiffness { get => stiffness; set => stiffness = value; }

    #endregion

    #region Initial setup

    void Start()
    {
        if (endPoint != null)
        {
            if (showRender)
            {
                InitCableParticles();
                InitLineRenderer();
            }
        }
    }

    /**
	 * Init cable particles
	 * 
	 * Creates the cable particles along the cable length
	 * and binds the start and end tips to their respective game objects.
	 */
    public void InitCableParticles()
    {
        if (this.showRender)
        {
            CableComponent cableCompEnd = endPoint.GetComponent<CableComponent>();
            cableCompEnd.showRender = false;
        }

        if (!diagramaSecuencial)
        {
            float dist = Vector3.Distance(endPoint.transform.position, transform.position);
            cableLength = (dist / 100) * 100; //+ offsetLength;
            //Debug.Log("Distancia cable generado: " + cableLength);
        }

        // Calculate segments to use
        if (totalSegments > 0)
        {
            segments = totalSegments;
        }
        else
        {
            segments = Mathf.CeilToInt(cableLength * segmentsPerUnit);
        }
        Vector3 cableDirection = (endPoint.transform.position - transform.position).normalized;
        //Debug.Log("cableDirection: " + cableDirection);
        float initialSegmentLength = cableLength / segments;
        points = new CableParticle[segments + 1];

        // Foreach point
        for (int pointIdx = 0; pointIdx <= segments; pointIdx++)
        {
            // Initial position
            Vector3 initialPosition = transform.position + (cableDirection * (initialSegmentLength * pointIdx));
            points[pointIdx] = new CableParticle(initialPosition);
        }

        // Bind start and end particles with their respective gameobjects

        CableParticle start = points[0];
        CableParticle end = points[segments];
        start.Bind(this.transform);
        end.Bind(endPoint.transform);
    }

    /**
	 * Initialized the line renderer
	 */

    public void InitLineRenderer(bool addLineRenderer = true)
    {
        line = this.gameObject.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
        }
        //line.SetWidth(cableWidth, cableWidth);
        //line.SetVertexCount(segments + 1);
        line.startWidth = cableWidth;
        line.positionCount = segments + 1;
        //line.material = cableMaterial;
        //line.colorGradient = Color.green;
        line.material = new Material(Shader.Find("Sprites/Default"));
        if(startColor == null)
        {
            startColor = AuxiliarModulos.startColor;
        }
        if (endColor == null)
        {
            endColor = AuxiliarModulos.endColor;
        }
        if (todoCableMismoColor)
        {
            line.startColor = endColor;
            line.endColor = endColor;
            line.SetColors(endColor, endColor);
        }
        else
        {
            line.startColor = startColor;
            line.endColor = endColor;
            line.SetColors(startColor, endColor);
        }
        line.GetComponent<Renderer>().enabled = true;
    }

    #endregion

    #region Render Pass

    void Update()
    {
        if (endPoint != null && line != null)
        {
            if (showRender)
            {
                RenderCable();
            }
        }
    }

    /**
	 * Render Cable
	 * 
	 * Update every particle position in the line renderer.
	 */
    void RenderCable()
    {
        //InitCableParticles();
        for (int pointIdx = 0; pointIdx < segments + 1; pointIdx++)
        {
            if (!diagramaSecuencial)
            {
                Vector3 position = points[pointIdx].Position;
                position.x = points[pointIdx].Position.x + offsetX;
                position.y = points[pointIdx].Position.y + offsetY;
                position.z = points[pointIdx].Position.z + offsetZ;
                line.SetPosition(pointIdx, position);
            }
            else
            {
                line.SetPosition(pointIdx, points[pointIdx].Position);
            }
            //line.SetPosition(pointIdx, points[pointIdx].Position);
        }
    }

    #endregion

    #region Verlet integration & solver pass

    void FixedUpdate()
    {
        if (endPoint != null && line != null)
        {
            if (showRender)
            {
                for (int verletIdx = 0; verletIdx < verletIterations; verletIdx++)
                {
                    VerletIntegrate();
                    SolveConstraints();
                }
            }
        }
    }

    /**
	 * Verler integration pass
	 * 
	 * In this step every particle updates its position and speed.
	 */
    void VerletIntegrate()
    {
        Vector3 gravityDisplacement = Time.fixedDeltaTime * Time.fixedDeltaTime * Physics.gravity;
        foreach (CableParticle particle in points)
        {
            particle.UpdateVerlet(gravityDisplacement);
        }
    }

    /**
	 * Constrains solver pass
	 * 
	 * In this step every constraint is addressed in sequence
	 */
    void SolveConstraints()
    {
        // For each solver iteration..
        for (int iterationIdx = 0; iterationIdx < solverIterations; iterationIdx++)
        {
            SolveDistanceConstraint();
            SolveStiffnessConstraint();
        }
    }

    #endregion

    #region Solver Constraints

    /**
	 * Distance constraint for each segment / pair of particles
	 **/
    void SolveDistanceConstraint()
    {
        float segmentLength = cableLength / segments;
        for (int SegIdx = 0; SegIdx < segments; SegIdx++)
        {
            CableParticle particleA = points[SegIdx];
            CableParticle particleB = points[SegIdx + 1];

            // Solve for this pair of particles
            SolveDistanceConstraint(particleA, particleB, segmentLength);
        }
    }

    /**
	 * Distance Constraint 
	 * 
	 * This is the main constrains that keeps the cable particles "tied" together.
	 */
    void SolveDistanceConstraint(CableParticle particleA, CableParticle particleB, float segmentLength)
    {
        // Find current vector between particles
        Vector3 delta = particleB.Position - particleA.Position;
        // 
        float currentDistance = delta.magnitude;
        float errorFactor = (currentDistance - segmentLength) / currentDistance;

        // Only move free particles to satisfy constraints
        if (particleA.IsFree() && particleB.IsFree())
        {
            particleA.Position += errorFactor * 0.5f * delta;
            particleB.Position -= errorFactor * 0.5f * delta;
        }
        else if (particleA.IsFree())
        {
            particleA.Position += errorFactor * delta;
        }
        else if (particleB.IsFree())
        {
            particleB.Position -= errorFactor * delta;
        }
    }

    /**
	 * Stiffness constraint
	 **/
    void SolveStiffnessConstraint()
    {
        float distance = (points[0].Position - points[segments].Position).magnitude;
        if (distance > cableLength)
        {
            foreach (CableParticle particle in points)
            {
                SolveStiffnessConstraint(particle, distance);
            }
        }
    }

    /**
	 * TODO: I'll implement this constraint to reinforce cable stiffness 
	 * 
	 * As the system has more particles, the verlet integration aproach 
	 * may get way too loose cable simulation. This constraint is intended 
	 * to reinforce the cable stiffness.
	 * // throw new System.NotImplementedException ();
	 **/
    void SolveStiffnessConstraint(CableParticle cableParticle, float distance)
    {


    }

    #endregion
}
