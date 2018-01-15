using PredictionApp.Service;
using System;
using System.Collections.Generic;

namespace PredictionApp.Presentation.Console.DataGeneration
{
    /// <summary>
    /// A clustering algorithm class 
    /// </summary>
    public class KMeansClusteringAlgorithm
    {
        /// <summary>
        /// Center location type of clustered groups
        /// </summary>
        public class Centroid
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public int MemberCount { get; private set; }

            /// <summary>
            /// Calculates the new center point with new coming coordinates
            /// </summary>
            /// <param name="newMemberLatitude">latitude value of new member of group</param>
            /// <param name="newMemberLongitude">longitude value of new member of group</param>
            public void RecalculateCenterByNewLocation(double newMemberLatitude, double newMemberLongitude)
            {
                /*Calculate new center point with using this algorithm: 
                (current center point * current member count + new member point * 1) / (current member count + 1)*/
                Latitude = (Latitude * MemberCount + newMemberLatitude) / (MemberCount + 1);
                Longitude = (Longitude * MemberCount + newMemberLongitude) / (MemberCount + 1);

                //Increase member count
                MemberCount++;
            }

            /// <summary>
            /// Assigns member count to 0.
            /// </summary>
            public void ResetMemberCount()
            {
                MemberCount = 0;
            }
        }

        /// <summary>
        /// Calculates euclidean distance
        /// </summary>
        /// <param name="firstLocationLatitude">firstLocationLatitude</param>
        /// <param name="firstLocationLongitude">firstLocationLongitude</param>
        /// <param name="secondLocationLatitude">secondLocationLatitude</param>
        /// <param name="secondLocationLongitude">secondLocationLongitude</param>
        /// <returns></returns>
        private double CalculateDistance(double firstLocationLatitude, double firstLocationLongitude, double secondLocationLatitude, double secondLocationLongitude)
        {
            // Calculation logic: result = Sqrt[ (location1.x -location2.x)^2 + (location1.y -location2.y)^2 ]

            var firstDimensionLength = firstLocationLatitude - secondLocationLatitude;
            var secondDimensionLength = firstLocationLongitude - secondLocationLongitude;

            return Math.Sqrt(Math.Pow(firstDimensionLength, 2) + Math.Pow(secondDimensionLength, 2));
        }

        /// <summary>
        /// Runs K-Means Clustering Algorithm
        /// </summary>
        /// <param name="customers">customer list to group</param>
        /// <param name="centroids">start-up center points</param>
        public void Run(List<CustomerDTO> customers, List<Centroid> centroids)
        {
            //Initialize start-up variables
            double minDistanceToCentroid;
            int indexOfClosestCentroid;

            //when this parameter is assigned as false, this means, grouping is completed.
            var nextIterationRequired = false;

            do
            {
                nextIterationRequired = false;

                for (int i = 0; i < customers.Count; i++)
                {
                    //Set min distance for this customer is infinitive.
                    minDistanceToCentroid = double.MaxValue;
                    indexOfClosestCentroid = -1;

                    //Calculate current location distance for each group center
                    for (int j = 0; j < centroids.Count; j++)
                    {
                        var currentDistance = CalculateDistance(customers[i].Latitude, customers[i].Longitude, centroids[j].Latitude, centroids[j].Longitude);

                        if (minDistanceToCentroid > currentDistance)
                        {
                            //Refresh closest group id for the location and collect the current distance for next comparison
                            indexOfClosestCentroid = j;
                            minDistanceToCentroid = currentDistance;
                        }
                    }

                    //if this 'if' condition runs,this mean nextIteration is required. Because, the group of current location is changed. after two successive iteration, groups of customers must be same to finish algorithm.

                    if (customers[i].LocationGroupId != indexOfClosestCentroid)
                    {
                        //Assign locations to closest group
                        customers[i].LocationGroupId = indexOfClosestCentroid;

                        nextIterationRequired = true;
                    }

                    //Recalculate location with new member
                    centroids[indexOfClosestCentroid].RecalculateCenterByNewLocation(customers[i].Latitude, customers[i].Longitude);
                }

                //reset member counts of groups.All that remains center coordinates of group.
                centroids.ForEach(x => x.ResetMemberCount());

            } while (nextIterationRequired);
        }
    }
}
