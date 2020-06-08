# Continuous beam analysis
Continuous beam analyzer is a simple tool to analyze statically indeterminate beams for shear force, bending moment and member forces.<br /><br /> Statically indeterminate beams are structures in which the number of reactions exceeds the number of independent equations of equilibrium. Numerically this problem can be solved with methods like moment distributioin method, matrix structural analysis, finite element methods etc.<br /><br />
This program uses finite element method to solve problems like below.<br /><br />
![](/CBAnalyzer/Images/cba_pic_0.png)<br /><br />
Modelled as shown below<br /><br />
![](/CBAnalyzer/Images/cba_pic_1.png)<br /><br />
![](/CBAnalyzer/Images/cba_pic_2.png)<br /><br />
![](/CBAnalyzer/Images/cba_pic_3.png)<br /><br />
Read through How to Use Tutorial_BeamAnalyzer_C.pdf for more information on how to use the software.<br /><br /><br /><br />
Lot of commercial software are available to solve the beam problem. This software has most of the features offered by commercial programs. However it lacks a design module, feature to add load cases, better management of live/dead loads, support sinking etc. I created this software back in my under grad days in 2008. The code is well organized but not very easy to read, my apologies for that.
