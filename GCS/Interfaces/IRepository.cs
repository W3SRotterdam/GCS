namespace W3S_GCS.Interfaces {
    interface IRepository<T> {
        T Get();
        T Set(T obj);
        T Create(T obj);
    }
}