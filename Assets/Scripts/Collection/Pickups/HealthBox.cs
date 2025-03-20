using UnityEngine;

public class HealthBox : PickUp
{
	[SerializeField] private HealBox _healBox;

	protected override void Collect(Creature collecter)
	{
		if (collecter == null)
			return;

		collecter.Heal(_healBox.Health);
		base.Collect(collecter);
	}
}
