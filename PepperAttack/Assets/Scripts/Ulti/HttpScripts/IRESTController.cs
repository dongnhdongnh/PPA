using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRESTController
{
    protected HttpRESTController httpRESTController;
    protected MonoBehaviour mono;
    public virtual void Init(HttpRESTController httpRESTController, MonoBehaviour mono)
    {
        this.httpRESTController = httpRESTController;
        this.mono = mono;
    }

}
