using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour {

    public Vector3 halfSize;

    MeshRenderer _mesh; //C# private C# feild

    public MeshRenderer mesh {//C#  property

        get {
            if (!_mesh) _mesh = GetComponent<MeshRenderer>();// "lazy" initailisation
            return _mesh;
        }
    }

    public Bounds bounds {
        get {
            return mesh.bounds;
        }

    }

    [HideInInspector] public bool isDoneChecking = false;
    bool isOverlapping = false;

    // Use this for initialization
    void Start() {
        CollisionController.Add(this);
    }

    void OnRemove() {
        CollisionController.Remove(this);
    }

    void OnDrawGizmos() {
        Gizmos.color = isOverlapping ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position, mesh.bounds.size);

    }

    // Update is called once per frame
    void Update() {
        isDoneChecking = false;
        isOverlapping = false;

    }

    /// <summary>
    /// Checks to see if some other AABB overlaps this AABB.
    /// </summary>
    /// <param name="other">the other aabb to check against.</param>
    /// <returns>if true the other AABBs overlap.</returns>
    public bool CheckOverlap(AABB other) {
        if (other.bounds.min.x > this.bounds.min.x) return false;
        if (other.bounds.max.x < this.bounds.min.x) return false;

        if (other.bounds.min.y > this.bounds.min.y) return false;
        if (other.bounds.max.y < this.bounds.min.y) return false;

        if (other.bounds.min.z > this.bounds.min.z) return false;
        if (other.bounds.max.z < this.bounds.min.z) return false;

        return true;
    }

    void OverlappingAABB(AABB other) {
       // print("Woot");
        isOverlapping = true;
    }


}
