# Martian-Robots
A coding task: The surface of Mars can be modelled by a rectangular grid around which robots are able to move according to instructions provided from Earth. A program that determines each sequence of robot positions and reports the final position of the robot.


## How to Run
1. ensure repo is cloned or downloaded via [this link](https://github.com/ryoung92/Martian-Robots)
2. To run the app
    1. Ensure that .net8 or higher is installed on your local machine (computer)
    2. Open either Terminal on macOS or CMD on windows
    3. navigate to where you cloned/downloaded and go to this folder ->

        ` cd ../src/MartianRobots `
    
    4. type into the console -> 
    
        ` dotnet run < robotInput.txt `
    (there is already a sample input in the robotInput.txt)

        ![expected output](/images/sampleOutput.png)

        expected default output using the input from robotInput.txt

    5. For custom inputs in the robotInput.txt file jump to [here](#customInput) or scrol below to the Custom Input section

3. To Run the unit tests
    1. navigate to where you cloned/downloaded and go to this folder ->

        ` cd ../src/TestMartianRobots `
    
    2. type into the console -> 
    
        ` dotnet test `

## <a name="customInput"></a>Custom Input!

In the robotInput.txt file the first line of input is the upper-right coordinates of the rectangular world, the lower-left
coordinates are assumed to be 0, 0.
The remaining input consists of a sequence of robot positions and instructions (two lines per
robot). A position consists of two integers specifying the initial coordinates of the robot and
an orientation (N, S, E, W), all separated by whitespace on one line. A robot instruction is a
string of the letters “L”
“R”
,
, and “F” on one line.
Each robot is processed sequentially, i.e., finishes executing the robot instructions before the
next robot begins execution.
The maximum value for any coordinate is 50.
All instruction strings will be less than 100 characters in length.

### Sample Input
5 3 \
1 1 E \
RFRFRFRF 

3 2 N \
FRRFLLFFRRFLL

0 3 W \
LLFFFLFLFL
