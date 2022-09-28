using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class GameStart : MonoBehaviour
{

    public GameObject worldUsers;

    public GameObject userPerfab;

    private string userID;

    private readonly Dictionary<string, GameObject> users = new Dictionary<string, GameObject>();

    void Start()
    {
        Debug.Log("start game componment");
        GEvents.OnJoinWorldNotifiy((name, obj) => {
            Debug.Log("new player join the world" + obj.Uid);
            if (!userID.Equals(obj.Uid))
            {
                if (!users.TryGetValue(obj.Uid, out GameObject userObj))
                {
                    GameObject newUser = Instantiate(userPerfab);
                    users[obj.Uid] = newUser;
                    newUser.transform.SetParent(worldUsers.transform);
                }
            }
        });

        GEvents.OnJoinWorldSuccess((name, obj) => {
            Debug.Log(obj.Uids.Count + " users in the world");
            userID = obj.Uid;
            foreach(string uid in obj.Uids)
            {
                if (!userID.Equals(uid))
                {
                    if (!users.TryGetValue(uid, out GameObject userObj))
                    {
                        Debug.Log("old player join the world" + uid);
                        GameObject newUser = Instantiate(userPerfab);
                        users[uid] = newUser;
                        newUser.transform.SetParent(worldUsers.transform);
                    }
                }
            }

        });

        GEvents.OnUpdateUserInWorld((name, obj) => {
            if (users.TryGetValue(obj.Uid, out GameObject userObj))
            {
                userObj.transform.position = new Vector3(obj.Postion.X, obj.Postion.Y, obj.Postion.Z);
                userObj.transform.rotation = new Quaternion(obj.Rotation.X, obj.Rotation.Y, obj.Rotation.Z, obj.Rotation.W );
            }
            else
            {
                if (!userID.Equals(obj.Uid))
                {
                    Debug.Log("new player join the world" + obj.Uid);
                    GameObject newUser = Instantiate(userPerfab);
                    users[obj.Uid] = newUser;
                    newUser.transform.SetParent(worldUsers.transform);
                }
            }
        });

        GEvents.OnLeaveWorldNotifiy((name, obj) => {
            if(users.TryGetValue(obj.Uid, out GameObject userObj))
            {
                Debug.Log(" player leave the world");
                Destroy(userObj);
            }
        });

        JoinWorld joinWorld = new JoinWorld();
        NetworkManager.Instance().SendByte(joinWorld.ToPacketData());
    }

}
