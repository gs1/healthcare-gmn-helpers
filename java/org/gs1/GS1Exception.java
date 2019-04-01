package org.gs1;

/**
 * A custom exception class to differentiate exceptions raised by the utility
 * class from other sources of error.
 *
 * @author (c) 2019 GS1 AISBL. All rights reserved.
 *
 */

public class GS1Exception extends Exception
{

    static final long serialVersionUID = 1L;

    public GS1Exception(String message) {
        super(message);
    }

}

