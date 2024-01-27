public interface IRoleBase
{
    public MoveState current { get; set; }
    public MoveState next { get; set; }
    public Train train { get; set; }

    public void Tick(float dt);

    public void GetOff(Train train)
    {
        //some code to detach this role

        if (next is null)
        {
            return;
        }

        next.stay = true;
    }

    public void GetOn(Train train)
    {
    }

    // should only be controlled by train
    public void SetNextByTrain(MoveState newState)
    {
        current = newState;
        next = newState;
    }

    public void SetNextByChoice(MoveState newState)
    {
        if (!current.stay)
        {
            // this is en error
            return;
        }

        next = newState;
    }
}