using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 此类用作JavaScript调用
    /// </summary>
    [ComVisible(true)]
    public class DataForScriptInUserOption
    {
        private Form _ownerForm;

        public DataForScriptInUserOption(Form ownerForm)
        {
            this._ownerForm = ownerForm;
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="userName"></param>
        //public void SetUserName(string userName,string passport)
        //{
        //    this._userName = userName;

        //    Service.User.RecordLogin(userName, passport);
        //}
        public void SetUserName(string userName)
        {
            this._userName = userName;

            Service.User.RecordLogin(userName, "");
        }

        /// <summary>
        /// 供登录界面进行注册,注册界面进行登录
        /// </summary>
        /// <param name="url"></param>
        public void Navigation(string url)
        {
            Service.Workbench.NavigationUrl(url);
            _ownerForm.Close();
        }
        ///// <summary>
        ///// 供用户登录调用
        ///// </summary>
        ///// <param name="userName"></param>
        //public void NavigationLogin(string userName,string passport)
        //{
        //    SetUserName(userName,passport);
            
        //    MessageService.Show("${res:user.login.successInfo}");
        //    this._ownerForm.Close();
        //}
        /// <summary>
        /// 供用户登录调用
        /// </summary>
        /// <param name="userName"></param>
        public void NavigationLogin(string userName)
        {
            SetUserName(userName);

            MessageService.Show("${res:user.login.successInfo}");
            this._ownerForm.Close();
        }

        /// <summary>
        /// 供用户注册成功后跳转到登录页调用
        /// </summary>
        /// <param name="url"></param>
        public void NavigationReg(string url)
        {
            Service.Workbench.NavigationUrl(url);
            MessageService.Show("${res:user.register.successInfo}");
            _ownerForm.Close();
        }
    }
}
