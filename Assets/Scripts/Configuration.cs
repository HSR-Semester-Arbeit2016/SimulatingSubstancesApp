using System;

[Serializable]
public class Configuration
{
    public Configuration()
    {
        BlurLevel = 0.0f;
        Delay = 0;
        MotionBlur = 0;
        RedColor = 0;
        Randomization = 0;
    }

    public string Name { get; set; }

    /// <summary>
    ///     Blur level from 0 to 15
    /// </summary>
    /// <value>The blur level.</value>
    public float BlurLevel { get; set; }

    /// <summary>
    ///     Tunnel level from 0 to 25
    /// </summary>
    /// <value>The tunnel level.</value>
    public float TunnelLevel { get; set; }

    /// <summary>
    ///     Boolean wrapper. Gets or sets the delay. 0 = off, 1 = on
    /// </summary>
    /// <value>The delay.</value>
    public int Delay { get; set; }

    /// <summary>
    ///     Boolean wrapper. Gets or sets the motion blur. 0 = off, 1 = on.
    /// </summary>
    /// <value>The motion blur.</value>
    public int MotionBlur { get; set; }

    /// <summary>
    ///     Boolean wrapper. Gets or sets the color of the red. 0 = off, 1 = on.
    /// </summary>
    /// <value>The color of the red.</value>
    public int RedColor { get; set; }

    /// <summary>
    ///     Boolean wrapper. Gets or sets the Randomization. 0 = off, 1 = on.
    /// </summary>
    /// <value>The Randomization.</value>
    public int Randomization { get; set; }

    public override string ToString()
    {
        return "Configuration Properties:\n" +
               "\nConfiguration Name: " + Name +
               "\nBlur Level: " + BlurLevel +
               "\nTunnel Level: " + TunnelLevel +
               "\nDelay Level: " + (Delay == 0 ? "Off" : "On") +
               "\nMotion Blur: " + (MotionBlur == 0 ? "Off" : "On") +
               "\nRedColor Distortion: " + (RedColor == 0 ? "Off" : "On") +
               "\nRandomization: " + (Randomization == 0 ? "Off" : "On");
    }
}