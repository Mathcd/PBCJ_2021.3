using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Essa classe é utilizada para estabilizar o movimento da câmera ao seguir o Player
/// </summary>
public class ArredondaPosCamera : CinemachineExtension
{
    public float PixelsPerUnit = 32;

    /* função analisa a posição da câmera de acordo com as coordenadas
     * e a arredonda, evitando que a imagem fique tremendo em caso de
     * pequenas variações na posição */
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam, 
        CinemachineCore.Stage stage, ref CameraState state, 
        float deltaTime)
    {
        if(stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.FinalPosition;      // posição inicial
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);  // posição arredondada
            state.PositionCorrection += pos2 - pos;     // a posição corrigida é a diferença entre as duas posições
        }
    }

    /* função para arredondar a posição */
    float Round(float x)
    {
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}
