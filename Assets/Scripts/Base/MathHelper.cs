using UnityEngine;
using System.Collections;

public class MathHelper {

    /// <summary>
    /// This Method calculates a point on the given ray, which has got the 
    ///     shortest distance to the given point.
    /// </summary>
    /// <param name="_Ray">The ray on which the point will be located.</param>
    /// <param name="_v3Point">The point to which the calculated point shall 
    ///     have the shortest distance to the given ray.</param>
    /// <returns>A Point which is located on the ray and has got the shortest 
    ///     distance to the given point.</returns>
    public static Vector3 v3GetNearestPointOnRay(Ray _Ray, Vector3 _v3Point) 
    {

        // A vector directing from the origin to the given point
        Vector3 v3DirectionRayOriginToPoint = _v3Point - _Ray.origin;

        float fDotProduct = 
            Vector3.Dot(_Ray.direction, v3DirectionRayOriginToPoint);

        // Projecting the prior Vector on the ray
        Vector3 v3ProjectedVector = _Ray.direction * fDotProduct;

        // Calculating the nearest point on the ray to the given point
        Vector3 v3NearestPointOnRay = _Ray.origin + v3ProjectedVector;

        return v3NearestPointOnRay;
    }

    /// <summary>
    /// This method checks if a given point lies between a given start and 
    ///     endpoint.
    /// </summary>
    /// <param name="_v3Point">The point to check.</param>
    /// <param name="_v3StartPoint">The startpoint, which is one border for
    ///     checking the point.</param>
    /// <param name="_v3Endpoint">The endpoint, which is the other border for 
    ///     checking the point.</param>
    /// <returns></returns>
    public static bool bLiesPointBetweenPoints(Vector3 _v3Point, Vector3 _v3StartPoint, Vector3 _v3Endpoint) 
    {

        // Length between point and startpoint
        float fLenthPointStartPoint = Vector3.Distance(_v3StartPoint, _v3Point);

        float fLengthEndPointStartPoint = Vector3.Distance(_v3StartPoint, _v3Endpoint);

        return fLenthPointStartPoint < fLengthEndPointStartPoint;
    }


    /// <summary>
    /// This function calculates the shortest distance between a point and a
    /// straight-line. If the point which lies on the orthogonal between the 
    /// given point and the given ray lies outside of the straight line it 
    /// will not be considered in the evaluation.
    /// </summary>
    /// <param name="_Straight">the straigt line to which the point shall have 
    ///     the shorstest distance</param>
    /// <param name="_v3NearestPointOnRay">the point on the ray, which has got 
    ///     the smallest dinstance to</param>
    /// <param name="_v3Point">the point to which the ray shall have the 
    ///     shortest distance</param>
    /// <returns>the shortest distance between the given point and the given 
    /// ray</returns>
    public static float fGetSmallestDistanceToStraight(Ray _Straight, Vector3 _v3NearestPointOnRay, Vector3 _v3Point) 
    {
        Vector3 v3RayEndPoint = _Straight.origin + _Straight.direction;

        float fDistancePointOrigin = Vector3.Distance(_Straight.origin, _v3Point);
        float fDistancePointEndPoint = Vector3.Distance(v3RayEndPoint, _v3Point);
        float fDistancePointNearestRayPoint = Vector3.Distance(_v3NearestPointOnRay, _v3Point);

        float[] arrDistances;

        // The shortest distance between given point and ray wil just be evaluated if the 
        // point lying on the ray is between the start- and end-point of the straight line.
        if (MathHelper.bLiesPointBetweenPoints(_v3Point, _Straight.origin, v3RayEndPoint)) 
        {
            arrDistances = new float[] { fDistancePointEndPoint,
                                         fDistancePointOrigin,
                                         fDistancePointNearestRayPoint };
        }
        else
        {
            arrDistances = new float[] { fDistancePointEndPoint,
                                         fDistancePointOrigin };
        }

        float fShorestDistance = float.MaxValue;

        for (int i = 0; i < arrDistances.Length; i++)
        {
            fShorestDistance = fShorestDistance > arrDistances[i] ? 
                                                  arrDistances[i] : 
                                                  fShorestDistance;
        }

        return fShorestDistance;
    }

}
