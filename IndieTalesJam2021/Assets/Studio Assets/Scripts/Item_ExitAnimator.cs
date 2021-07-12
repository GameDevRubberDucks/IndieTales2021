using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Item_ExitAnimator : MonoBehaviour
{
    public Vector2 m_animationDurationRange;
    public Vector2 m_rotationRange;

    public void StartAnimation()
    {
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim()
    {
        GetComponent<Grid_Snapper>().enabled = false;
        GetComponent<Tile_Aligner>().enabled = false;

        GetComponent<Tile_VFX>().PlayFX(Tile_VFXType.Removal);
        
        Vector3 endPos = GameObject.FindWithTag("ExitPoint").transform.position;

        float animationDuration = Random.Range(m_animationDurationRange.x, m_animationDurationRange.y);
        var newSequence = DOTween.Sequence();
        newSequence.Append(transform.DOMove(endPos, animationDuration));
        newSequence.Join(transform.DOScale(0.0f, animationDuration));
        newSequence.Join(transform.DORotate(new Vector3(0.0f, 0.0f, Random.Range(m_rotationRange.x, m_rotationRange.y)), animationDuration));

        yield return newSequence.WaitForCompletion();

        GetComponent<Tile_VFX>().PlayFX(Tile_VFXType.Sold);

        var moneyButton = GameObject.FindGameObjectWithTag("MoneyButton").transform;
        if (!DOTween.IsTweening(moneyButton))
        {
            moneyButton.DOPunchScale(Vector3.one * 0.1f, 0.1f, 10, 1.0f);
            GameObject.FindGameObjectWithTag("MoneyFX").GetComponent<ParticleSystem>().Play();
        }

        Destroy(this.gameObject);
    }
}
