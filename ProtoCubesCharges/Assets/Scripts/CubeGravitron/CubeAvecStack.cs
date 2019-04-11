using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAvecStack : MonoBehaviour
{
    //public float breakForce;

    //[SerializeField] private bool materialised = false;
    //[SerializeField] private Collider hologrammeCollider;
    //[SerializeField] private PhysicMaterial defaultMaterial;
    //[SerializeField] private PhysicMaterial stackedMaterial;

    //private Charges charges;
    //private Rigidbody rb;
    //private CubeFeedbackManager feedbackManager;

    //[SerializeField] private bool stacked = false;
    //[SerializeField] private List<Cube> stackedCubes = new List<Cube>();
    //[SerializeField] private List<FixedJoint> joints = new List<FixedJoint>();

    //public bool IsStacked
    //{
    //    get { return stacked; }
    //}

    //public int Poids
    //{
    //    get { return charges.CurrentPoids; }
    //}

    //public int NbCharges
    //{
    //    get
    //    {
    //        if (materialised)
    //            return 0;
    //        else
    //            return charges.CurrentCharge;
    //    }
    //}

    //public int NbChargesAjoutables
    //{
    //    get
    //    {
    //        if (materialised)
    //            return 0;
    //        else
    //            return charges.MaxCharges - charges.CurrentCharge;
    //    }
    //}

    //public bool Materialised
    //{
    //    get { return materialised; }
    //}


    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    charges = GetComponent<Charges>();
    //    hologrammeCollider.material = defaultMaterial;
    //    feedbackManager = GetComponent<CubeFeedbackManager>();
    //    feedbackManager.Init(charges.CurrentCharge, charges.CurrentPoids, materialised);
    //}

    //private void FixedUpdate()
    //{
    //    Cube toRemove = null;
    //    foreach (Cube cube in stackedCubes)
    //    {
    //        float differenceY = cube.transform.position.y - transform.position.y;
    //        if (differenceY * cube.Poids <= 0)
    //        {
    //            cube.Stacked(false);
    //            RemoveJointWith(cube);
    //            toRemove = cube;
    //        }
    //    }
    //    if (toRemove != null)
    //        stackedCubes.Remove(toRemove);
    //}

    //public void Alourdir()
    //{
    //    charges.RetraitChargeNegative();
    //    feedbackManager.Charges = charges.CurrentCharge;
    //    feedbackManager.Poids = charges.CurrentPoids;
    //}

    //public void Alleger()
    //{
    //    charges.AjoutChargeNegative();
    //    feedbackManager.Charges = charges.CurrentCharge;
    //    feedbackManager.Poids = charges.CurrentPoids;
    //}

    //public void SwitchMaterialisation()
    //{
    //    materialised = !materialised;
    //    rb.isKinematic = materialised;
    //    feedbackManager.Materialise = materialised;
    //}

    //public void RAPActivated()
    //{
    //    Stacked(true);
    //}

    //public void RAPDeactivated()
    //{
    //    Stacked(false);
    //}

    //public void Stacked(bool state)
    //{
    //    stacked = state;
    //    hologrammeCollider.material = state ? stackedMaterial : defaultMaterial;
    //    GetComponent<Gravity>().enabled = !state;
    //    if (state)
    //    {
    //        foreach (Cube cube in stackedCubes)
    //        {
    //            if (!cube.IsStacked)
    //            {
    //                cube.Stacked(true);
    //                CreateStackJoint(cube);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        foreach (Cube cube in stackedCubes)
    //            cube.Stacked(false);

    //        foreach (FixedJoint joint in joints)
    //            Destroy(joint);
    //        joints.Clear();
    //    }
    //}

    //public void AttractionRepulsion(Vector3 hitNormal, bool isAttracting)
    //{
    //    hitNormal = isAttracting ? hitNormal * 150f : hitNormal * -150f; //force de propulsion

    //    rb.velocity = Vector3.zero;
    //    rb.AddForce(hitNormal, ForceMode.Impulse); //Application de la force
    //}

    //private void TestStackingObject(ContactPoint[] contacts)
    //{
    //    for (int i = 0; i < contacts.Length; i++) //Pour chaque point de contact
    //    {
    //        Cube contactCube = contacts[i].otherCollider.GetComponentInParent<Cube>();
    //        if (contactCube != null)
    //        {
    //            ///contacts[i].normal.y est négative si on touche la face inférieure de l'autre cube (elle "sort" du cube touché)
    //            if (contacts[i].normal.y * contactCube.Poids < 0) //touche par la bonne face en fonction du poids du cube qui touche (celle du haut si poids 2, celle du bas si poids -2)
    //            {
    //                if (!stackedCubes.Contains(contactCube))
    //                {
    //                    stackedCubes.Add(contactCube);
    //                    if (stacked)
    //                    {
    //                        contactCube.Stacked(true);
    //                        CreateStackJoint(contactCube);
    //                    }
    //                }
    //            }
    //        }

    //    }
    //}


    //private void CreateStackJoint(Cube cube)
    //{
    //    FixedJoint joint = gameObject.AddComponent<FixedJoint>();
    //    joint.connectedBody = cube.GetComponent<Rigidbody>();
    //    joint.breakForce = breakForce;
    //    joint.enablePreprocessing = false;
    //    joints.Add(joint);
    //}


    //private void OnJointBreak(float breakForce)
    //{
    //    FixedJoint brokenJoint = null;
    //    foreach (FixedJoint joint in joints)
    //    {
    //        if (joint.currentForce.magnitude >= this.breakForce - 1)
    //        {
    //            Cube lostCube = joint.connectedBody.GetComponent<Cube>();
    //            lostCube.Stacked(false);
    //            stackedCubes.Remove(lostCube);
    //            brokenJoint = joint;
    //        }
    //    }

    //    joints.Remove(brokenJoint);
    //    Debug.Log("Break ! joints restants : " + joints.Count);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    TestStackingObject(collision.contacts);
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    Cube other = collision.gameObject.GetComponent<Cube>();
    //    if (other != null)
    //    {
    //        if (stackedCubes.Contains(other) && !stacked) // Si on touchait ce cube mais qu'on est pas en mode "attaché empilé"
    //            stackedCubes.Remove(other); //on l'oublie
    //    }
    //}

    //private bool JointsContains(Cube cube)
    //{
    //    foreach (FixedJoint joint in joints)
    //    {
    //        if (joint != null)
    //            if (joint.connectedBody.GetComponent<Cube>() == cube)
    //                return true;
    //    }
    //    return false;
    //}

    //private void RemoveJointWith(Cube cube)
    //{
    //    FixedJoint toRemove = null;
    //    foreach (FixedJoint joint in joints)
    //    {
    //        if (joint != null)
    //            if (joint.connectedBody.GetComponent<Cube>() == cube)
    //                toRemove = joint;
    //    }
    //    joints.Remove(toRemove);
    //    Destroy(toRemove);
    //}
}
