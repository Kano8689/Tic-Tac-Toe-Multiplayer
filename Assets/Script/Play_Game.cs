using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Play_Game : MonoBehaviourPunCallbacks
{
    public Text WinBoard;
    public GameObject btnHolder;
    public Button[] btnAry;
    public Sprite O, X;
    Text userName, oppUser;
    string Username, Oppuser;
    PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        //#01 'a'
        btnHolder = GameObject.Find("BtnHolder");

        Username = PhotonNetwork.PlayerList[0].NickName;
        Oppuser = PhotonNetwork.PlayerList[1].NickName;

        //Debug.Log("1=" + Username);
        //Debug.Log("2=" + Oppuser);

        userName = GameObject.Find("User Name").GetComponent<Text>();
        userName.text = Username;

        oppUser = GameObject.Find("Opp User").GetComponent<Text>();
        oppUser.text = Oppuser;


        PV = GetComponent<PhotonView>();    
    }

    // Update is called once per frame
    void Update()
    {
        Username = PhotonNetwork.PlayerList[0].NickName;
        userName = GameObject.Find("User Name").GetComponent<Text>();
        userName.text = Username;

        Oppuser = PhotonNetwork.PlayerList[1].NickName;
        oppUser = GameObject.Find("Opp User").GetComponent<Text>();
        oppUser.text = Oppuser;
    }
    public void onClickBtn(int n)
    {
        string s = "";
        Sprite img= null;
        int i = 0;
        if (PhotonNetwork.PlayerList[0].NickName == PhotonNetwork.NickName)
        {
            Debug.Log("Click O");
            s = "0";
            img = O;
            //btnHolder.transform.GetChild(n).GetComponentInChildren<Text>().text = "0";
            //btnHolder.transform.GetChild(n).GetComponentInChildren<Button>().interactable = false;
        }
        else
        {
            Debug.Log("Click X");
            s = "X";
            img = X;
            //btnHolder.transform.GetChild(n).GetComponentInChildren<Text>().text = "X";
            //btnHolder.transform.GetChild(n).GetComponentInChildren<Button>().interactable = false;
        }
        //btnHolder.transform.GetChild(n).GetComponentInChildren<Text>().text = s;
        //btnHolder.transform.GetChild(n).GetComponentInChildren<Button>().interactable = false;
        btnAry[n].GetComponent<Image>().sprite = img;
        btnAry[n].interactable = false;
        PV.RPC("syncValue", RpcTarget.All, n, s);
    }
    void CheckWin()
    {
        if ((btnAry[0].GetComponent<Image>().sprite == O && btnAry[1].GetComponent<Image>().sprite == O && btnAry[2].GetComponent<Image>().sprite == O) || 
            (btnAry[3].GetComponent<Image>().sprite == O && btnAry[4].GetComponent<Image>().sprite == O && btnAry[5].GetComponent<Image>().sprite == O) || 
            (btnAry[6].GetComponent<Image>().sprite == O && btnAry[7].GetComponent<Image>().sprite == O && btnAry[8].GetComponent<Image>().sprite == O) || 
            (btnAry[0].GetComponent<Image>().sprite == O && btnAry[3].GetComponent<Image>().sprite == O && btnAry[6].GetComponent<Image>().sprite == O) || 
            (btnAry[1].GetComponent<Image>().sprite == O && btnAry[4].GetComponent<Image>().sprite == O && btnAry[7].GetComponent<Image>().sprite == O) || 
            (btnAry[2].GetComponent<Image>().sprite == O && btnAry[5].GetComponent<Image>().sprite == O && btnAry[8].GetComponent<Image>().sprite == O) || 
            (btnAry[0].GetComponent<Image>().sprite == O && btnAry[4].GetComponent<Image>().sprite == O && btnAry[8].GetComponent<Image>().sprite == O) || 
            (btnAry[2].GetComponent<Image>().sprite == O && btnAry[4].GetComponent<Image>().sprite == O && btnAry[6].GetComponent<Image>().sprite == O))
        {
            PV.RPC("getWin", RpcTarget.All, "O");
            //WinBoard.text = "O is Win";
        }
        else if ((btnAry[0].GetComponent<Image>().sprite == X && btnAry[1].GetComponent<Image>().sprite == X && btnAry[2].GetComponent<Image>().sprite == X) || 
            (btnAry[3].GetComponent<Image>().sprite == X && btnAry[4].GetComponent<Image>().sprite == X && btnAry[5].GetComponent<Image>().sprite == X) || 
            (btnAry[6].GetComponent<Image>().sprite == X && btnAry[7].GetComponent<Image>().sprite == X && btnAry[8].GetComponent<Image>().sprite == X) || 
            (btnAry[0].GetComponent<Image>().sprite == X && btnAry[3].GetComponent<Image>().sprite == X && btnAry[6].GetComponent<Image>().sprite == X) || 
            (btnAry[1].GetComponent<Image>().sprite == X && btnAry[4].GetComponent<Image>().sprite == X && btnAry[7].GetComponent<Image>().sprite == X) || 
            (btnAry[2].GetComponent<Image>().sprite == X && btnAry[5].GetComponent<Image>().sprite == X && btnAry[8].GetComponent<Image>().sprite == X) || 
            (btnAry[0].GetComponent<Image>().sprite == X && btnAry[4].GetComponent<Image>().sprite == X && btnAry[8].GetComponent<Image>().sprite == X) || 
            (btnAry[2].GetComponent<Image>().sprite == X && btnAry[4].GetComponent<Image>().sprite == X && btnAry[6].GetComponent<Image>().sprite == X))
        {
            PV.RPC("getWin", RpcTarget.All, "X");
            //WinBoard.text = "X is Win";
        }
    }
    [PunRPC]
    void getWin(string win)
    {
        WinBoard.text = win + " is Win";
    }

    [PunRPC]
    void syncValue(int n, string s)
    {
        if (s == "0")
        {
            btnAry[n].GetComponent<Image>().sprite = O;
            btnAry[n].interactable = false;
            //btnHolder.transform.GetChild(n).GetComponentInChildren<Text>().text = s;
            //btnHolder.transform.GetChild(n).GetComponentInChildren<Button>().interactable = false;
        }
        else
        {
            btnAry[n].GetComponent<Image>().sprite = X;
            btnAry[n].interactable = false;
        }
        CheckWin();
    }
}
