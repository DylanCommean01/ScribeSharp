namespace ScribeSharp
{
    public abstract class User
    {
        public abstract bool IsStudent();
        public abstract bool IsTeacher();
        public abstract void StartConversation();
        public abstract override string ToString();
    }
}
