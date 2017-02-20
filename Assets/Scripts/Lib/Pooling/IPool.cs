using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on TheLiquidFire's: https://theliquidfire.wordpress.com/2015/07/06/object-pooling/
public interface IPool {

  GameObject PopObject(); 
  GameObject PushObject(); 

}
