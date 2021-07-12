using UnityEngine;

public enum Tile_VFXType
{
    Placement,
    Removal,
    Sold,
    Stockpile_Removal,

    Count
}

public class Tile_VFX : MonoBehaviour
{
    public ParticleSystem m_vfxPlacement;
    public ParticleSystem m_vfxRemoval;
    public ParticleSystem m_vfxSold;

    public void PlayFX(Tile_VFXType _vfxType)
    {
        switch(_vfxType)
        {
            case Tile_VFXType.Placement:
                m_vfxPlacement.Play();
                break;

            case Tile_VFXType.Removal:
                m_vfxRemoval.Play();
                m_vfxRemoval.transform.parent = null;
                Destroy(m_vfxRemoval.gameObject, 1.5f);
                break;

            case Tile_VFXType.Sold:
                m_vfxSold.Play();
                m_vfxSold.transform.parent = null;
                m_vfxSold.transform.localScale = Vector3.one;
                m_vfxSold.transform.right = Vector3.right;
                Destroy(m_vfxSold.gameObject, 1.5f);
                break;

            case Tile_VFXType.Stockpile_Removal:
                m_vfxPlacement.Play();
                m_vfxPlacement.transform.parent = null;
                Destroy(m_vfxPlacement.gameObject, 1.5f);
                break;

            default:
                break;
        }
    }
}
