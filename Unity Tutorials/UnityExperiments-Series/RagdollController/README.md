<h1>Ragdoll Controller</h1>
<p>This is the first episode in the "Unity Experiments" series.</p>
<p>I'll sharing with you a script to toggle ragdoll mode on and off. When toggling it off, the character will return to its last pose.</p>
<h2>How to Use:</h2>
<p>There are two ways, you can either download the package in this directory. Or, you can get the script "RagdollController.cs" and use it yourself.</p>
<p>If you decided to use the script, go through these steps:</p>
<ul>
	<li>Attach the script to a ragdoll character that has an <i>Animator</i> component as well.</li>
	<li>Attach the <i>Hips</i> bone to the Hips variable.</li>
	<li>Set <i>rotationSpeed</i> and <i>movementSpeed</i> to how fast the bones will move and rotate once you disable the Ragdoll mode.</li>
</ul>
<h2>Enabling and Disabling:</h2>
<p>You can either enable or disable the Ragdoll using the two methods <i>EnableRagdoll()</i> and <i>DisableRagdoll()</i></p>
<p>If you want to check if the Ragdoll is activated, use the <i>isRagdollActive()</i> method.</p>