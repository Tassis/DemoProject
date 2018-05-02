namespace TControls.Core{

    public class TStaticHelper<T> : TSingleton<T> where T : class, new() {

        protected bool hasInited { get; private set; }

        public void Init()
        {
            if(hasInited)
                return;

            hasInited = true;
            OnInit();
        }

        public void UnInit()
        {
            if(!hasInited)
                return;
                
            hasInited = false;
            OnUnInit();
        }

        protected virtual void OnInit(){ } 
        protected virtual void OnUnInit() { }

    }

}
