using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInfoScreenController {

    void Initialize (TutorialScreenController tutorialScreenController);
    void Play ();
    void Stop ();
	
}
