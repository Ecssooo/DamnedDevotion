using UnityEngine;

/// <summary>
///  Utiliser pour setup les scripts / button qui ont besoin de fonction exterieur type : GameManager, AudioManager...
/// </summary>
public class SetScreen : MonoBehaviour
{
    /// <summary>
    ///  Appeler au chargement de l'ecran
    /// </summary>
    public virtual void OnLoad(){}
}
