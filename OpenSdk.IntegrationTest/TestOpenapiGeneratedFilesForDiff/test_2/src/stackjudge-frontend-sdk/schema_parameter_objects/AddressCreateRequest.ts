import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class AddressCreateRequest
{
    public constructor(
        private fullAddress: string,
        private markerLat: number,
        private markerLng: number,
        private manualMarkerLat: number,
        private manualMarkerLng: number,
    ) {
    }
}
