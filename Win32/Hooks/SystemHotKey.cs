﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Win32.API;
using Win32.Enums;

namespace Win32.Hooks
{
    /// <summary>
    /// 系统热键
    /// </summary>
    public class SystemHotKey:NativeWindow
    {
        private const int WM_HOTKEY = 0x0312;// 热键消息
        private IntPtr _hwnd;
        private Dictionary<int, Action> hotkeydictionary = new Dictionary<int, Action>();

        /// <summary>
        ///初始化
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        public SystemHotKey(IntPtr hwnd)
        {
            _hwnd = hwnd;
            this.AssignHandle(_hwnd);
        }

        /// <summary>
        ///初始化
        /// </summary>
         ~SystemHotKey()
        {
            if (hotkeydictionary.Count > 0)
            {
                foreach (var item in hotkeydictionary)
                {
                    UnRegHotKey(item.Key);
                }
            }
            ReleaseHandle();
        }

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        /// <param name="keyModifiers">组合键</param>
        /// <param name="key">热键</param>
        internal void RegHotKey(IntPtr hwnd, int hotKeyId, KeyModifiers keyModifiers, Keys key)
        {
            if (!Win32Api.RegisterHotKey(hwnd, hotKeyId, keyModifiers, key))
            {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode == 1409)
                {
                    throw new Exception("RegHotKey failed,HotKey has  be Occupied ");
                }
                else
                {
                    throw new Exception("RegHotKey failed,error code: " + errorCode);
                }
            }
        }
        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        internal void UnRegHotKey(IntPtr hwnd, int hotKeyId)
        {
            //注销指定的热键
          Win32Api. UnregisterHotKey(hwnd, hotKeyId);
        }

        /// <summary>
        /// 添加热键
        /// </summary>
        /// <param name="hotKeyId"></param>
        /// <param name="keyModifiers"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public void AddHotKey(int hotKeyId, KeyModifiers keyModifiers, Keys key,Action action)
        {
            if (_hwnd != null)
            {
                if (!hotkeydictionary.Keys.Contains(hotKeyId))
                {
                    RegHotKey(_hwnd, hotKeyId, keyModifiers, key);
                    hotkeydictionary.Add(hotKeyId, action);
                } 
            }
        }
        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hotKeyId"></param>
        /// <param name="keyModifiers"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public void UnRegHotKey(int hotKeyId)
        {
            if (_hwnd != null)
            {
                UnRegHotKey(_hwnd, hotKeyId);

                if (hotkeydictionary.Keys.Contains(hotKeyId))
                {
                    hotkeydictionary.Remove(hotKeyId);
                }
            }
        }

        #region 消息处理
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                //热键消息
                case WM_HOTKEY:
                    {
                        int KeyInfo = m.WParam.ToInt32();
                        if (hotkeydictionary.Count > 0)
                        {
                            hotkeydictionary[KeyInfo]?.Invoke();

                        }
                        break;
                    }
            }
           
            base.WndProc( ref m);
        
        }

        #endregion

    }
}
