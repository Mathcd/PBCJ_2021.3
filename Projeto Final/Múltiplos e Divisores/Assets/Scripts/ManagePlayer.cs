using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBackend;

public class ManagePlayer : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool VerificaCartaCompativel(int cartaAnteriorValor, char cartaAnteriorTipo, int cartaAtualValor)
    {
        switch (cartaAnteriorTipo)
        {
            case 'M':
                if (cartaAtualValor % cartaAnteriorValor == 0)
                    return true;
                break;
            case 'D':
                if (cartaAnteriorValor % cartaAtualValor == 0)
                    return true;
                break;
            case 'A':
                if (cartaAnteriorValor % cartaAtualValor == 0 | cartaAtualValor % cartaAnteriorValor == 0)
                    return true;
                break;
        }
        return false;
    }


}
