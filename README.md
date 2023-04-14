# AR: Homography Matrix

- CalculateHomography.cs is for all the functions,
- Part1.cs is for the test and log.

## 1.1
Write a C# function that given a set of point correspondences 𝑆𝑖=[𝑥𝑖𝑦𝑖]𝑇 and 𝐼𝑖=[𝑢𝑖𝑣𝑖]𝑇 calculates  the  corresponding  homography matrix.You  can  assume  that  there  are  equal  number of  points  for 𝑆𝑖 and 𝐼𝑖 and  they  are  in  correspondence. This  function  should  use  a  non-linear optimization method to calculate the homography. 
- I used 4 source points and 4 image points and used the Ah = 0 formula. Then, SVD function is applied to Matrix A. I used the VT matrix from the SVD and got its transpose. Finally, the result Homography Matrix is the 3x3 matrix arranged from the last row of the transposed matrix.

## 1.2
Write another C# function that given a scene point [𝑥𝑖𝑦𝑖] and a homography matrix, calculates the projection of the given point onto the target image.
- I used an example point as a scene point. I used the homography matrix to transform the coordinates onto the target image. This is made by multiplying the homography matrix by a matrix containing the coordinates of the scene point. The result of the multiplication is the matrix containing the projected coordinates.

## 1.3
Write  another  C#  function  that  given  an  image  point [𝑢𝑖𝑣𝑖] and  a  homography  matrix, calculates the projection of the given point ontothe scene.
- I used an example point as an image point. I used the inverse of the homography matrix to transform the coordinates onto the scene. This is, again, made by multiplying the homography matrix by a matrix containing the coordinates of the image point. The result of the multiplication is the matrix containing the projected coordinates.


