
namespace Architecture
{
    public abstract class Interactor
    {
        public virtual void OnCreate() { } // когда все интеракторы и репо созданы.
        public virtual void Initialize() { } //когда все интеракторы и репо сделали OnCreate();

        public virtual void OnStart() { } // Когда все интеракторы и репо проинициализированы.

    }
}
