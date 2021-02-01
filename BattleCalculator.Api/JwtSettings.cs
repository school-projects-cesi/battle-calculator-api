namespace BattleCalculator.Api
{
    public class JwtSettings
    {
		public string SecretKey { get; set; }
		public int Lifespan { get; set; }
	}
}
