namespace GDAPI.Objects.General
{
    /// <summary>Represents a progress reporting function, that reports the progress based on the current step and the total steps of a procedure.</summary>
    /// <param name="currentStep">The current step that is being reported as progress.</param>
    /// <param name="totalSteps">The total steps that have to be performed in the procedure for it to be considered complete.</param>
    public delegate void ProgressReporter(int currentStep, int totalSteps);
}
