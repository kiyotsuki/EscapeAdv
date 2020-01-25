using UnityEngine;

public class SaveData
{
	public int SlotNo { get; private set; }

	public string SlotLabel { get; private set; }

	public long SaveTime { get; private set; }

	public bool IsEnable { get { return SaveTime > 0; } }

	public ParamPlayer.ID[] ActivePlayers { get; private set; }

	public SaveData(int slotNo)
	{
		SlotNo = slotNo;
	}
}

