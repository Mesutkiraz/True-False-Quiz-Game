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
    private List<Soru> cevaplanmam�ssorular;
    private Soru gecerlisoru;
    [SerializeField]
    private GameObject dogruButon, yanlisButon,sonucpanel,birinciyildiz,ikinciyildiz,ucuncuyildiz;
    int dogruAdet, yanl�sAdet,toplampuan;
    void Start()
    {
        if (cevaplanmam�ssorular==null || cevaplanmam�ssorular.Count == 0)
        {
            cevaplanmam�ssorular = sorular.ToList<Soru>();
        }
        dogruAdet = 0;
        yanl�sAdet = 0;
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
        int randomindex = Random.Range(0, cevaplanmam�ssorular.Count);
        gecerlisoru = cevaplanmam�ssorular[randomindex];
        sorutext.text = gecerlisoru.soru;
        if (gecerlisoru.dogrumu)
        {
            dogruCevaptext.text = "DO�RU CEVAPLADINIZ";
            yanlisCevaptext.text = "YANLI� CEVAPLADINIZ";
        }
        else
        {
            dogruCevaptext.text = "YANLI� CEVAPLADINIZ";
            yanlisCevaptext.text = "DO�RU CEVAPLADINIZ";
        }
    }
    public void yenidenoyna()
    {
        SceneManager.LoadScene("SampleScene");
    }
    IEnumerator sorulararas�bekleroutine()
    {
        cevaplanmam�ssorular.Remove(gecerlisoru);
        yield return new WaitForSeconds(1f);
        if (cevaplanmam�ssorular.Count<=0)
        {
            sonucpanel.SetActive(true);
            dogruadet.text = dogruAdet.ToString();
            sonucpuan.text = toplampuan.ToString();
            yanlisadet.text = yanl�sAdet.ToString();
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
   public void dogrubutonabas�ld�()
    {
        if (gecerlisoru.dogrumu)
        {
            dogruAdet++;
            toplampuan += 100;
            
        }
        else
        {
            yanl�sAdet++;
           
        }
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(1000, 1f);
        StartCoroutine(sorulararas�bekleroutine());
    }
   public void yanl�sbutonabas�ld�()
    {
        if (!gecerlisoru.dogrumu)
        {
            dogruAdet++;
            toplampuan += 100;
           
        }
        else
        {
            yanl�sAdet++;
           
        }
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-1000, 1f);
        StartCoroutine(sorulararas�bekleroutine());
    }
}
