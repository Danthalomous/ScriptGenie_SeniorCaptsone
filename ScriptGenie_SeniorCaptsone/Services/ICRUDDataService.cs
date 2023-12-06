namespace ScriptGenie_SeniorCaptsone.Services
{
    public interface ICRUDDataService <T>
    {
        LinkedList<T> Create(int userID, T newModel);
        LinkedList<T> FetchAll(int userID);
        LinkedList<T> Update(int userID, T newModel);
        LinkedList<T> Delete(int userID, int organizationID);
    }
}
