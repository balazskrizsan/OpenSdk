import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class GetCompanyParams implements IOpenSdkGetable
{
    public constructor(
        private seekId: number,
        private limit: number,
        private requestRelationIds: number[],
        private navigationId: number,
    ) {
    }

    public toOpenSdkGet()
    {
        return {
            "seekId": this.seekId,
            "limit": this.limit,
            "requestRelationIds": this.requestRelationIds,
            "navigationId": this.navigationId,
        }
    }
}
