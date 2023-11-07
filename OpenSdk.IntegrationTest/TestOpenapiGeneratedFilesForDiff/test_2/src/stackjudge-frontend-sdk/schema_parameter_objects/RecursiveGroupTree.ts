import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class RecursiveGroupTree
{
    public constructor(
        private recursiveGroup: valueObject.RecursiveGroup,
        private children: valueObject.RecursiveGroupTree[],
    ) {
    }
}
