import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class Address
{
    public constructor(
        private id: number,
        private companyId: number,
        private fullAddress: string,
        private markerLat: number,
        private markerLng: number,
        private manualMarkerLat: number,
        private manualMarkerLng: number,
        private createdAt: string,
        private createdBy: string,
    ) {
    }
}
