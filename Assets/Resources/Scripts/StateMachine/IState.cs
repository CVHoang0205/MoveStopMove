

public interface IState<Boss>
{

    public void OnEnter(Boss boss) 
    { }

    public void OnExecute(Boss boss) 
    { }

    public void OnExit(Boss boss) 
    { }
   
}
