//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using NAudio.Dsp;

namespace DialogEngine.Controls.VoiceRecorder
{
    public class SampleAggregator
    {
        #region - fields -

        private float mVolumeLeftMaxValue;
        private float mVolumeLeftMinValue;
        private float mVolumeRightMaxValue;
        private float mVolumeRightMinValue;
        private Complex[] mChannelData;
        private int mBufferSize;
        private int mBinaryExponentitation;
        private int mChannelDataPosition;

        #endregion

        #region - constructor -

        public SampleAggregator(int bufferSize)
        {
            this.mBufferSize = bufferSize;
            mBinaryExponentitation = (int)Math.Log(bufferSize, 2);
            mChannelData = new Complex[bufferSize];
        }

        #endregion

        #region - properties -

        public float LeftMaxVolume
        {
            get { return mVolumeLeftMaxValue; }
        }

        public float LeftMinVolume
        {
            get { return mVolumeLeftMinValue; }
        }

        public float RightMaxVolume
        {
            get { return mVolumeRightMaxValue; }
        }

        public float RightMinVolume
        {
            get { return mVolumeRightMinValue; }
        }

        #endregion

        #region - public methods -

        public void Clear()
        {
            mVolumeLeftMaxValue = float.MinValue;
            mVolumeRightMaxValue = float.MinValue;
            mVolumeLeftMinValue = float.MaxValue;
            mVolumeRightMinValue = float.MaxValue;
            mChannelDataPosition = 0;
        }
             
        /// <summary>
        /// Add a sample value to the aggregator.
        /// </summary>
        /// <param name="value">The value of the sample.</param>
        public void Add(float leftValue, float rightValue)
        {            
            if (mChannelDataPosition == 0)
            {
                mVolumeLeftMaxValue = float.MinValue;
                mVolumeRightMaxValue = float.MinValue;
                mVolumeLeftMinValue = float.MaxValue;
                mVolumeRightMinValue = float.MaxValue;
            }

            // Make stored channel data stereo by averaging left and right values.
            mChannelData[mChannelDataPosition].X = (leftValue + rightValue) / 2.0f;
            mChannelData[mChannelDataPosition].Y = 0;
            mChannelDataPosition++;

            mVolumeLeftMaxValue = Math.Max(mVolumeLeftMaxValue, leftValue);
            mVolumeLeftMinValue = Math.Min(mVolumeLeftMinValue, leftValue);
            mVolumeRightMaxValue = Math.Max(mVolumeRightMaxValue, rightValue);
            mVolumeRightMinValue = Math.Min(mVolumeRightMinValue, rightValue);

            if (mChannelDataPosition >= mChannelData.Length)
            {
                mChannelDataPosition = 0;
            }
        }

        /// <summary>
        /// Performs an FFT calculation on the channel data upon request.
        /// </summary>
        /// <param name="fftBuffer">A buffer where the FFT data will be stored.</param>
        public void GetFFTResults(float[] fftBuffer)
        {
            Complex[] _channelDataClone = new Complex[mBufferSize];
            mChannelData.CopyTo(_channelDataClone, 0);
            FastFourierTransform.FFT(true, mBinaryExponentitation, _channelDataClone);
            for (int i = 0; i < _channelDataClone.Length / 2; i++)
            {
                // Calculate actual intensities for the FFT results.
                fftBuffer[i] = (float)Math.Sqrt(_channelDataClone[i].X * _channelDataClone[i].X + _channelDataClone[i].Y * _channelDataClone[i].Y);
            }
        }

        #endregion
    }
}
