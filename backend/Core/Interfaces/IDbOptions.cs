namespace Core.Interfaces;

public interface IDbOptions
{
    public string MasterConnectionString { get; set; }
    public string ReplicaConnectionString { get; set; }
}