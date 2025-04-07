public interface IBarChangeable
{
	public event System.Action<float, float> Changed;

	public void TriggerChange(float currentValue, float maxValue);
}
