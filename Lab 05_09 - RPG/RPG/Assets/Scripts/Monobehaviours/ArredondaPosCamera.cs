using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Essa classe � utilizada para estabilizar o movimento da c�mera ao seguir o Player
/// </summary>
public class ArredondaPosCamera : CinemachineExtension
{
    public float PixelsPerUnit = 32;

    /* fun��o analisa a posi��o da c�mera de acordo com as coordenadas
     * e a arredonda, evitando que a imagem fique tremendo em caso de
     * pequenas varia��es na posi��o */
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam, 
        CinemachineCore.Stage stage, ref CameraState state, 
        float deltaTime)
    {
        if(stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.FinalPosition;      // posi��o inicial
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);  // posi��o arredondada
            state.PositionCorrection += pos2 - pos;     // a posi��o corrigida � a diferen�a entre as duas posi��es
        }
    }

    /* fun��o para arredondar a posi��o */
    float Round(float x)
    {
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}
