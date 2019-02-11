﻿namespace OSCXR
{
    using System.Collections.Generic;
    using UnityEngine;

    public class BaseOscController : MonoBehaviour
    {

        // #### Unity Inspector Items ####
        // ###############################

        [Header("OSC Config")]

        [Tooltip("The controller ID is the first argument sent from every message")]
        public int controllerID;

        [Tooltip("Names of OSC Receivers to send messages to. Leave empty to send to all receivers in Transmitter DB")]
        public List<string> receiverNames = new List<string>();

        [Tooltip("Base OSC Address. Leave Blank for using default base address of widget")]
        public string oscAddress;
        public string OscAddress
        {
            get { return oscAddress; }
            set { oscAddress = value; }
        }

        public void SendOSCMessage(string address, params object[] values)
        {
            // Add controller ID to params
            object[] tmp = new object[values.Length + 1];
            tmp[0] = controllerID;
            values.CopyTo(tmp, 1);

            if (receiverNames.Count > 0)
            {
                foreach (var cname in receiverNames)
                {
                    OSCTransmitManager.Transmitter.SendOscMessage(cname, address, tmp); // include controller ID for every message
                }
            }
            else
            {
                OSCTransmitManager.Transmitter.SendOscMessageAll(address, tmp); // include controller ID for every message
            }
        }
    }
}