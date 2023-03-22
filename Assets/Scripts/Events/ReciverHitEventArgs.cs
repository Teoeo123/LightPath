using System;
using UnityEngine;

public class ReciverHitEventArgs : EventArgs
    {
        public int laserindex { get; set; }
         public GameObject hitobject { get; set; }
    }

