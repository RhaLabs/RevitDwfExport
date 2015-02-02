# Revit Dwf Export

This is an addin for Autodesk Revit.  The code is a modified version of the sample exporter by Autodesk.

## Why We Need It

When you want to print a single sheet from Revit all is well and good.  You get to pick the file name you like. 
However, if you want to print an entire drawing set you are out of luck.  Revit decides what the sheet names are and all you get
todo is make up a prefix.

I don't like having to rename a bunch of files, especially when Revit has all of the data and could easliy name the files
appropriately.

## How it Works

There are built-in parameters in Revit like `sheet_name` and `sheet_number` but for us we need an additional parameter
`sequence_number`  (Since we collaborate with others there are many versions of this parameter).  Basically the *sequence*
parameter is used to prefix the file names so that regardless of name the sheets will appear in the correct order in the 
file system.  For example if we have 4 sheets C1 - Cover Sheet, A1 - Floor Plan, A2 - Elevations, LS1 - Life Safety Plan the file 
system would order them like:

*   A1 - Floor Plan
*   A2 - Elevations
*   C1 - Cover Sheet
*   LS1 - Life Safety Plan

When we really want them in this order:

*   C1 - Cover Sheet
*   LS1 - Life Safety Plan
*   A1 - Floor Plan
*   A2 - Elevations

We use the sequence to prefix all sheets like this:

*   0000-C1 - Cover Sheet
*   0010-LS1 - Life Safety Plan
*   0020-A1 - Floor Plan
*   0040-A2 - Elevations

## Page Size

The page size should be automatically get detected by the boundaries of your titleblock.  
