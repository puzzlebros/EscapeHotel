﻿using UnityEngine;
using System.Collections;
using Photon.Pun;

public class Grab : Pickable, IPunObservable
{
    public float throwForce = 100;
    //public Vector3 objectOffset;
    //public AudioClip[] soundToPlay;
    //private AudioSource audio;
    //public int dmg;

    public override void OnPickUp()
    {
        base.OnPickUp();

        Picked = true;

        Rigid.isKinematic = false;

    }

    public override void OnRelease()
    {
        base.OnRelease();

        Picked = false;

        Rigid.isKinematic = false;
        Rigid.useGravity = true;
        Rigid.AddForce(Camera.main.transform.forward * throwForce);

    }

    public override void OnHold()
    {
        base.OnHold();

        Picked = true;
    }

    void Update()
    {
        if (Picked)
        {
            Carry();
            RotateObject();
        }
    }

    void RotateObject()
    {
        gameObject.transform.Rotate(5, 10, 15);
    }

    void Carry()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Camera.main.transform.position + Camera.main.transform.forward * distance, Time.deltaTime * smoothing);
        //gameObject.transform.rotation = Quaternion.identity;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(Rigid.isKinematic);
            stream.SendNext(Rigid.useGravity);
            stream.SendNext(Picked);
        }
        else
        {
            // Network player, receive data
            Rigid.isKinematic = (bool)stream.ReceiveNext();
            Rigid.useGravity = (bool)stream.ReceiveNext();
            Picked = (bool)stream.ReceiveNext();
        }
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if(other.gameObject.tag == "CardSlot")
    //    {
    //        base.OnRelease();
    //        Picked = false;
    //        Rigid.isKinematic = true;
    //        //IsPickable = false;

    //        gameObject.transform.SetParent(other.transform);
    //        transform.position = other.gameObject.transform.position + new Vector3(0,0,0.2f);
    //        transform.rotation = other.gameObject.transform.rotation;

    //    }
    //}

    //public override void OnPlace()
    //{

    //}

    //void RandomAudio()
    //{
    //    if (audio.isPlaying)
    //    {
    //        return;
    //    }
    //    audio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
    //    audio.Play();

    //}
}