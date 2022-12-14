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
    private List<Soru> cevaplanmamıssorular;
    private Soru gecerlisoru;
    [SerializeField]
    private GameObject dogruButon, yanlisButon,sonucpanel,birinciyildiz,ikinciyildiz,ucuncuyildiz;
    int dogruAdet, yanlısAdet,toplampuan;
    void Start()
    {
        if (cevaplanmamıssorular==null || cevaplanmamıssorular.Count == 0)
        {
            cevaplanmamıssorular = sorular.ToList<Soru>();
        }
        dogruAdet = 0;
        yanlısAdet = 0;
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
        int randomindex = Random.Range(0, cevaplanmamıssorular.Count);
        gecerlisoru = cevaplanmamıssorular[randomindex];
        sorutext.text = gecerlisoru.soru;
        if (gecerlisoru.dogrumu)
        {
            dogruCevaptext.text = "DOĞRU CEVAPLADINIZ";
            yanlisCevaptext.text = "YANLIŞ CEVAPLADINIZ";
        }
        else
        {
            dogruCevaptext.text = "YANLIŞ CEVAPLADINIZ";
            yanlisCevaptext.text = "DOĞRU CEVAPLADINIZ";
        }
    }
    public void yenidenoyna()
    {
        SceneManager.LoadScene("SampleScene");
    }
    IEnumerator sorulararasıbekleroutine()
    {
        cevaplanmamıssorular.Remove(gecerlisoru);
        yield return new WaitForSeconds(1f);
        if (cevaplanmamıssorular.Count<=0)
        {
            sonucpanel.SetActive(true);
            dogruadet.text = dogruAdet.ToString();
            sonucpuan.text = toplampuan.ToString();
            yanlisadet.text = yanlısAdet.ToString();
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
   public void dogrubutonabasıldı()
    {
        if (gecerlisoru.dogrumu)
        {
            dogruAdet++;
            toplampuan += 100;
            
        }
        else
        {
            yanlısAdet++;
           
        }
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(1000, 1f);
        StartCoroutine(sorulararasıbekleroutine());
    }
   public void yanlısbutonabasıldı()
    {
        if (!gecerlisoru.dogrumu)
        {
            dogruAdet++;
            toplampuan += 100;
           
        }
        else
        {
            yanlısAdet++;
           
        }
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-1000, 1f);
        StartCoroutine(sorulararasıbekleroutine());
    }
}
