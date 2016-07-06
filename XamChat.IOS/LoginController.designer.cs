// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace XamChat.IOS
{
    [Register ("LoginController")]
	partial class LoginController: UIViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView indicator { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton loginButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField password { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField username { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (indicator != null) {
                indicator.Dispose ();
                indicator = null;
            }

            if (loginButton != null) {
                loginButton.Dispose ();
                loginButton = null;
            }

            if (password != null) {
                password.Dispose ();
                password = null;
            }

            if (username != null) {
                username.Dispose ();
                username = null;
            }
        }
    }
}