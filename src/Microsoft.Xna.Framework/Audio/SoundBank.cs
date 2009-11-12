#region License
/*
MIT License
Copyright © 2006 The Mono.Xna Team

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

using System;
using System.IO;

namespace Microsoft.Xna.Framework.Audio
{
    public class SoundBank : IDisposable
    {

        string name;
        string targetname;
        string[] cues;
        AudioEngine audioengine;

        #region Events

        public event EventHandler Disposing;

        #endregion Events


        #region Public Properties

        public bool IsDisposed
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsInUse
        {
            get { throw new NotImplementedException(); }
        }

        #endregion


        #region Constructors

        public SoundBank(AudioEngine audioEngine, string filename)
        {
            audioengine = audioEngine;
            BinaryReader soundbankreader = new BinaryReader(new FileStream(filename, FileMode.Open));
            //byte[] identifier = soundbankreader.ReadBytes(4);

            soundbankreader.BaseStream.Seek(30, SeekOrigin.Begin);
            int cuelength = soundbankreader.ReadInt32();

            soundbankreader.BaseStream.Seek(42, SeekOrigin.Begin);
            int cueoffset = soundbankreader.ReadInt32();

            soundbankreader.BaseStream.Seek(74, SeekOrigin.Begin);
            name = System.Text.Encoding.UTF8.GetString(soundbankreader.ReadBytes(64)).Replace("\0","");

            targetname = System.Text.Encoding.UTF8.GetString(soundbankreader.ReadBytes(64)).Replace("\0", "");

            soundbankreader.BaseStream.Seek(cueoffset, SeekOrigin.Begin);

            cues = System.Text.Encoding.UTF8.GetString(soundbankreader.ReadBytes(cuelength)).Split('\0');
        }

        #endregion Constructors


        #region Destructors

        ~SoundBank()
        {
        }

        #endregion Destructors


        #region Public Methods

        public static bool operator !=(SoundBank left, SoundBank right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(SoundBank left, SoundBank right)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public Cue GetCue(string name)
        {
            for (int i = 0; i < cues.Length - 1; i++)
            {
                if (cues[i] == name)
                {
                    foreach (WaveBank wavebank in audioengine.Wavebanks)
                    {
                        if (wavebank.BankName == targetname)
                        {
                            return new Cue(cues[i], wavebank.buffer[i], audioengine.source);
                        }
                    }
                }
            }
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public void PlayCue(string name)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods


        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        protected void raise_Disposing(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }
        
        #endregion Protected Methods
    }
}
