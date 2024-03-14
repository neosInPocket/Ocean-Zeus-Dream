using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[SaveDuringPlay]
public class CinemachineCameraFreeze : CinemachineExtension
{
	protected override void PostPipelineStageCallback(
		CinemachineVirtualCameraBase vcam,
		CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
	{
		if (stage == CinemachineCore.Stage.Body)
		{
			Vector3 position = state.RawPosition;
			Quaternion rotation = state.RawOrientation;
			rotation.x = 0;
			rotation.y = 0;
			rotation.z = 0;
			position.x = 0;
			state.RawPosition = position;
		}
	}
}

