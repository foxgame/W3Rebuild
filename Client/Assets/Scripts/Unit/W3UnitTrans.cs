using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class W3Unit
{
    
    public void trans( int uid )
    {
        int n1x = ( unitData.pathW / 2 - unitData.pathX ) - unitData.pathMinX + 1;
        int n1z = ( unitData.pathH / 2 - unitData.pathZ ) - unitData.pathMinZ + 1;

        int n2x = unitData.pathMaxX - ( unitData.pathW / 2 - unitData.pathX ) + 1;
        int n2z = unitData.pathMaxZ - ( unitData.pathH / 2 - unitData.pathZ ) + 1;

        int npx = 0;
        int npz = 0;

        W3UnitBalanceConfigData d2 = W3UnitBalanceConfig.instance.getData( uid );
        byte c = (byte)( d2.collision / 16 );
        if ( d2.collision % 16 != 0 )
        {
            c++;
        }
        if ( d2.collision % 32 == 0 )
        {
            c++;
        }
        if ( c == 1 )
        {
            c = 2;
        }


        if ( W3PathFinder.instance.findNearPosTrans( lastPosition.x , lastPosition.z , n1x , n1z , n2x , n2z , c , out npx , out npz ) )
        {
            W3UnitManager.instance.createUnitA( baseData.playerID , uid , npx ,
                npz , 0.0f , false );
        }
    }


    public void updateTrans( float delay )
    {

    }

}

