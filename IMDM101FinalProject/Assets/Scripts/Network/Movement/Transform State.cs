using UnityEngine;
using Unity.Netcode;

public class TransformState : INetworkSerializable
{
	public TransformState() { }
	

	public int Tick;
	public Vector3 position;
	public Quaternion rotation;
	public bool hasStartedMoving;
	public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
	{

		if (serializer.IsReader)
		{
			var reader = serializer.GetFastBufferReader();
			reader.ReadValueSafe(out Tick);
			reader.ReadValueSafe(out position);
			reader.ReadValueSafe(out rotation);
			reader.ReadValueSafe(out hasStartedMoving);
		}
		else
		{
			var writer = serializer.GetFastBufferWriter();
			writer.WriteValueSafe(Tick);
			writer.WriteValueSafe(position);
			writer.WriteValueSafe(rotation);
			writer.WriteValueSafe(hasStartedMoving);
		}
	}
}
