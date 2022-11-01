using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Text sorutext,dogruCevaptext,yanlisCevaptext,yanlisadet,dogruadet,sonucpuan;
    public Soru[] sorular;
    private List<Soru> cevaplanmamýssorular;
    private Soru gecerlisoru;
    [SerializeField]
    private GameObject dogruButon, yanlisButon,sonucpanel,birinciyildiz,ikinciyildiz,ucuncuyildiz;
    int dogruAdet, yanlýsAdet,toplampuan;
    void Start()
    {
        if (cevaplanmamýssorular==null || cevaplanmamýssorular.Count == 0)
        {
            cevaplanmamýssorular = sorular.ToList<Soru>();
        }
        dogruAdet = 0;
        yanlýsAdet = 0;
        toplampuan = 0;
        birinciyildiz.SetActive(false);
        ikinciyildiz.SetActive(false);
        ucuncuyildiz.SetActive(false);
        rastgeleIndex();
    }
    void rastgeleIndex()
    {
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(320, .2f);
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-320, .2f);
        int randomindex = Random.Range(0, cevaplanmamýssorular.Count);
        gecerlisoru = cevaplanmamýssorular[randomindex];
        sorutext.text = gecerlisoru.soru;
        if (gecerlisoru.dogrumu)
        {
            dogruCevaptext.text = "DOÐRU CEVAPLADINIZ";
            yanlisCevaptext.text = "YANLIÞ CEVAPLADINIZ";
        }
        else
        {
            dogruCevaptext.text = "YANLIÞ CEVAPLADINIZ";
            yanlisCevaptext.text = "DOÐRU CEVAPLADINIZ";
        }
    }
    public void yenidenoyna()
    {
        SceneManager.LoadScene("SampleScene");
    }
    IEnumerator sorulararasýbekleroutine()
    {
        cevaplanmamýssorular.Remove(gecerlisoru);
        yield return new WaitForSeconds(1f);
        if (cevaplanmamýssorular.Count<=0)
        {
            sonucpanel.SetActive(true);
            dogruadet.text = dogruAdet.ToString();
            sonucpuan.text = toplampuan.ToString();
            yanlisadet.text = yanlýsAdet.ToString();
            if (dogruAdet==1)
            {
                birinciyildiz.SetActive(true);
            }
            else if (dogruAdet==2)
            {
                birinciyildiz.SetActive(true);
                ikinciyildiz.SetActive(true);
            }
            else if (dogruAdet==3)
            {
                birinciyildiz.SetActive(true);
                ikinciyildiz.SetActive(true);
                ucuncuyildiz.SetActive(true);
            }
        }
        else
        {
            rastgeleIndex();
        }

    }
   public void dogrubutonabasýldý()
    {
        if (gecerlisoru.dogrumu)
        {
            dogruAdet++;
            toplampuan += 100;
            
        }
        else
        {
            yanlýsAdet++;
           
        }
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(1000, 1f);
        StartCoroutine(sorulararasýbekleroutine());
    }
   public void yanlýsbutonabasýldý()
    {
        if (!gecerlisoru.dogrumu)
        {
            dogruAdet++;
            toplampuan += 100;
           
        }
        else
        {
            yanlýsAdet++;
           
        }
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-1000, 1f);
        StartCoroutine(sorulararasýbekleroutine());
    }
}
