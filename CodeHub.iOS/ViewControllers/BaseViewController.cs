﻿using System;
using ReactiveUI;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using Foundation;

namespace CodeHub.iOS.ViewControllers
{
    public abstract class BaseViewController : ReactiveViewController, IActivatable
    {
        private readonly ISubject<bool> _appearingSubject = new Subject<bool>();
        private readonly ISubject<bool> _appearedSubject = new Subject<bool>();
        private readonly ISubject<bool> _disappearingSubject = new Subject<bool>();
        private readonly ISubject<bool> _disappearedSubject = new Subject<bool>();

        #if DEBUG
        ~BaseViewController()
        {
            Console.WriteLine("All done with " + GetType().Name);
        }
        #endif

        public IObservable<bool> Appearing
        {
            get { return _appearingSubject.AsObservable(); }
        }

        public IObservable<bool> Appeared
        {
            get { return _appearedSubject.AsObservable(); }
        }

        public IObservable<bool> Disappearing
        {
            get { return _disappearingSubject.AsObservable(); }
        }

        public IObservable<bool> Disappeared
        {
            get { return _disappearedSubject.AsObservable(); }
        }

        public void OnActivation(Action<Action<IDisposable>> d)
        {
            this.WhenActivated(d);
        }

        protected BaseViewController()
        {
            CommonConstructor();
        }

        protected BaseViewController(string nib, NSBundle bundle)
            : base(nib, bundle)
        {
            CommonConstructor();
        }

        private void CommonConstructor()
        {
            this.WhenActivated(_ => { });
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _appearingSubject.OnNext(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            _appearedSubject.OnNext(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _disappearingSubject.OnNext(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            _disappearedSubject.OnNext(animated);
        }
    }
}

